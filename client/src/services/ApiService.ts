import { dark } from "@mui/material/styles/createPalette";

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

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      if (response.status == 204) {
        return response as unknown as T;
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
