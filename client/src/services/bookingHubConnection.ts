import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
const { env } = require("process");

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(";")[0]
    : "https://localhost:5001";

export const createHubconnection = () => {
  const url = `${target}/bookingHub`;
  const connection = new HubConnectionBuilder()
    .withUrl(url)
    .configureLogging(LogLevel.Information)
    .build();
  return connection;
};
