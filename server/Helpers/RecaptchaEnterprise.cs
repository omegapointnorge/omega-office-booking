using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.RecaptchaEnterprise.V1;
using Grpc.Auth;

public class RecaptchaEnterprise
{
    public const double ReCaptchaThreshold = 0.7;

    private readonly IConfiguration _configuration;
    private readonly ILogger<RecaptchaEnterprise> _logger;
    private readonly RecaptchaEnterpriseServiceClient _client;
    private readonly string _projectId;
    private readonly string _credentialsJson;

    private const string DefaultRecaptchaKey = "6Lc1tV8pAAAAABKV5g3LrYZNzUx1KGQkYHR-hSzo";
    private const string DefaultRecaptchaActionName = "BOOKING";

    public RecaptchaEnterprise(IConfiguration configuration, ILogger<RecaptchaEnterprise> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Initialize project ID and credentials JSON
        _projectId = GetConfigurationValue("GOOGLE-CLOUD-PROJECT", "Error: GOOGLE-CLOUD-PROJECT environment variable is not set.");
        _credentialsJson = GetConfigurationValue("GOOGLE-APPLICATION-CREDENTIALS", "Error: GOOGLE-APPLICATION-CREDENTIALS environment variable is not set.");

        // Additional setup in the constructor
        _client = InitializeRecaptchaClient();
    }

    public double CreateAssessment(string token, string recaptchaKey = DefaultRecaptchaKey, string recaptchaAction = DefaultRecaptchaActionName)
    {
        // Build the assessment request
        var createAssessmentRequest = BuildAssessmentRequest(token, recaptchaKey, recaptchaAction);

        try
        {
            var response = _client.CreateAssessment(createAssessmentRequest);

            // Validate the token
            if (!response.TokenProperties.Valid)
            {
                _logger.LogError($"The CreateAssessment call failed because the token was: {response.TokenProperties.InvalidReason}");
                throw new InvalidOperationException($"The CreateAssessment call failed because the token was: {response.TokenProperties.InvalidReason}");
            }

            // Check if the expected action was executed
            if (response.TokenProperties.Action != recaptchaAction)
            {
                _logger.LogError($"The action attribute in reCAPTCHA tag is: {response.TokenProperties.Action}\nThe action attribute in the reCAPTCHA tag does not match the action you are expecting to score");
                throw new InvalidOperationException($"The action attribute in reCAPTCHA tag is: {response.TokenProperties.Action}\nThe action attribute in the reCAPTCHA tag does not match the action you are expecting to score");
            }

            return response.RiskAnalysis.Score;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred during the CreateAssessment call: {ex.Message}");
            throw;
        }
    }

    private string GetConfigurationValue(string key, string errorMessage)
    {
        var value = _configuration.GetValue<string>(key);
        if (string.IsNullOrEmpty(value))
        {
            _logger.LogError(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }
        return value;
    }

    private RecaptchaEnterpriseServiceClient InitializeRecaptchaClient()
    {
        var credential = GoogleCredential.FromJson(_credentialsJson);
        var clientBuilder = new RecaptchaEnterpriseServiceClientBuilder
        {
            ChannelCredentials = credential.ToChannelCredentials()
        };
        return clientBuilder.Build();
    }

    private CreateAssessmentRequest BuildAssessmentRequest(string token, string recaptchaKey, string recaptchaAction)
    {
        var projectName = new ProjectName(_projectId);

        return new CreateAssessmentRequest
        {
            Assessment = new Assessment
            {
                Event = new Event
                {
                    SiteKey = recaptchaKey,
                    Token = token,
                    ExpectedAction = recaptchaAction
                }
            },
            ParentAsProjectName = projectName
        };
    }
}