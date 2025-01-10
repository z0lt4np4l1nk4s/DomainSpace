import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from "@microsoft/signalr";
import { UserDataService } from "./UserDataService";
import { HUB_URL } from "../config";

export class SignalRService {
  private connection: HubConnection | null = null;

  private static instance: SignalRService;

  constructor() {
    if (!SignalRService.instance) {
      SignalRService.instance = this;
    }
    return SignalRService.instance;
  }

  buildConnection(): HubConnection {
    if (!this.connection) {
      this.connection = new HubConnectionBuilder()
        .withUrl(HUB_URL, {
          accessTokenFactory: () => {
            return UserDataService.getUserToken()!.token;
          },
          transport: HttpTransportType.WebSockets,
          withCredentials: true,
        })
        .withAutomaticReconnect()
        .build();

      this.connection
        .start()
        .then(() => console.log("SignalR Connection Started"))
        .catch((error) => {
          console.error("SignalR Connection Error:", error);
        });
    }
    return this.connection;
  }

  public stopConnection() {
    if (this.connection) {
      this.connection
        .stop()
        .catch((err) => console.error("SignalR Stop Error:", err));

      this.connection = null;
    }
  }
}
