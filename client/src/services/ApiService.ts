type ApiOptionsType = {
  method: string;
  headers: { [key: string]: string };
  body?: string | null;
};
class ApiService {
  async fetchData<T>(url: string, method: string, body: unknown | null = null) {
    const options: ApiOptionsType = {
      method,
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    };

    if (body !== null) {
      options.body = JSON.stringify(body);
    }

    try {
      const response = await fetch(url, options);
      /* Forslag: 
      {
        data: responseData as T,
        ok: true,
        status: response.status,
        statusText: response.statusText,
        corr....Id: for Errorhandling
      }
      */
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      return (await response.json()) as T;
    } catch (e) {
      console.error("Error fetching data:", e);
      throw e;
    }
  }
}
const apiService = new ApiService();
export default apiService;
