// data/signalR.js 文件
import * as signalR from "@microsoft/signalr";
import { VueCookieNext as $cookie } from 'vue-cookie-next';


let connectionInstance = null;

export default function startConnection() {
    if (!connectionInstance) {
        let userId, username;
        userId = $cookie.getCookie('Id');
        username = $cookie.getCookie('name');
        connectionInstance = new signalR.HubConnectionBuilder()
        .withUrl(`https://localhost:7048/chatHub?userId=${userId}&username=${username}`, {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

        connectionInstance.start().then(() => {
            console.log('SignalR 成功連線');
        }).catch(err => {
            console.error('SignalR connection failed:', err.toString());
        });
        
        
    }

    return connectionInstance;
}
