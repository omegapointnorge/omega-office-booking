/**
 * Populate bookings table with appropriate description
 * @param {Object} bookings ID token bookings
 * @returns bookingsObject
 */
export const createbookingsTable = (bookings) => {
    let bookingsObj = {};
    let index = 0;

    Object.keys(bookings).forEach((key) => {
        if (typeof bookings[key] !== 'string' && typeof bookings[key] !== 'number') return;
        switch (key) {
            case 'aud':
                populateClaim(
                    key,
                    bookings[key],
                    "Identifies the intended recipient of the token. In ID tokens, the audience is your app's Application ID, assigned to your app in the Azure portal.",
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'iss':
                populateClaim(
                    key,
                    bookings[key],
                    'Identifies the issuer, or authorization server that constructs and returns the token. It also identifies the Azure AD tenant for which the user was authenticated. If the token was issued by the v2.0 endpoint, the URI will end in /v2.0. The GUID that indicates that the user is a consumer user from a Microsoft account is 9188040d-6c67-4c5b-b112-36a304b66dad.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'iat':
                populateClaim(
                    key,
                    changeDateFormat(bookings[key]),
                    'Issued At indicates when the authentication for this token occurred.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'nbf':
                populateClaim(
                    key,
                    changeDateFormat(bookings[key]),
                    'The nbf (not before) claim identifies the time (as UNIX timestamp) before which the JWT must not be accepted for processing.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'exp':
                populateClaim(
                    key,
                    changeDateFormat(bookings[key]),
                    "The exp (expiration time) claim identifies the expiration time (as UNIX timestamp) on or after which the JWT must not be accepted for processing. It's important to note that in certain circumstances, a resource may reject the token before this time. For example, if a change in authentication is required or a token revocation has been detected.",
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'name':
                populateClaim(
                    key,
                    bookings[key],
                    "The name claim provides a human-readable value that identifies the subject of the token. The value isn't guaranteed to be unique, it can be changed, and it's designed to be used only for display purposes. The profile scope is required to receive this claim.",
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'preferred_username':
                populateClaim(
                    key,
                    bookings[key],
                    'The primary username that represents the user. It could be an email address, phone number, or a generic username without a specified format. Its value is mutable and might change over time. Since it is mutable, this value must not be used to make authorization decisions. It can be used for username hints, however, and in human-readable UI as a username. The profile scope is required in order to receive this claim.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'nonce':
                populateClaim(
                    key,
                    bookings[key],
                    'The nonce matches the parameter included in the original /authorize request to the IDP. If it does not match, your application should reject the token.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'oid':
                populateClaim(
                    key,
                    bookings[key],
                    'The oid (user’s object id) is the only claim that should be used to uniquely identify a user in an Azure AD tenant. The token might have one or more of the following claim, that might seem like a unique identifier, but is not and should not be used as such.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'tid':
                populateClaim(
                    key,
                    bookings[key],
                    'The tenant ID. You will use this claim to ensure that only users from the current Azure AD tenant can access this app.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'upn':
                populateClaim(
                    key,
                    bookings[key],
                    '(user principal name) – might be unique amongst the active set of users in a tenant but tend to get reassigned to new employees as employees leave the organization and others take their place or might change to reflect a personal change like marriage.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'email':
                populateClaim(
                    key,
                    bookings[key],
                    'Email might be unique amongst the active set of users in a tenant but tend to get reassigned to new employees as employees leave the organization and others take their place.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'acct':
                populateClaim(
                    key,
                    bookings[key],
                    'Available as an optional claim, it lets you know what the type of user (homed, guest) is. For example, for an individual’s access to their data you might not care for this claim, but you would use this along with tenant id (tid) to control access to say a company-wide dashboard to just employees (homed users) and not contractors (guest users).',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'sid':
                populateClaim(key, bookings[key], 'Session ID, used for per-session user sign-out.', index, bookingsObj);
                index++;
                break;
            case 'sub':
                populateClaim(
                    key,
                    bookings[key],
                    'The sub claim is a pairwise identifier - it is unique to a particular application ID. If a single user signs into two different apps using two different client IDs, those apps will receive two different values for the subject claim.',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'ver':
                populateClaim(
                    key,
                    bookings[key],
                    'Version of the token issued by the Microsoft identity platform',
                    index,
                    bookingsObj
                );
                index++;
                break;
            case 'uti':
            case 'rh':
                index++;
                break;
            case '_claim_names':
            case '_claim_sources':
            default:
                populateClaim(key, bookings[key], '', index, bookingsObj);
                index++;
        }
    });

    return bookingsObj;
};

/**
 * Populates claim, description, and value into an bookingsObject
 * @param {String} claim
 * @param {String} value
 * @param {String} description
 * @param {Number} index
 * @param {Object} bookingsObject
 */
const populateClaim = (claim, value, description, index, bookingsObject) => {
    let bookingsArray = [];
    bookingsArray[0] = claim;
    bookingsArray[1] = value;
    bookingsArray[2] = description;
    bookingsObject[index] = bookingsArray;
};

/**
 * Transforms Unix timestamp to date and returns a string value of that date
 * @param {String} date Unix timestamp
 * @returns
 */
const changeDateFormat = (date) => {
    let dateObj = new Date(date * 1000);
    return `${date} - [${dateObj.toString()}]`;
};