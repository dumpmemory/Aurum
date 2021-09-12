namespace Aurum.Configurator

open System.Collections.Generic
open FSharp.Json

module Transport =
    [<RequireQualifiedAccess>]
    type Networks =
        | WS
        | GRPC
        | TCP
        | KCP
        | DomainSocket
        | HTTP
        | QUIC

    [<RequireQualifiedAccess>]
    type Security =
        | None
        | TLS
        | XTLS

    type WebSocketObject =
        { path: string
          maxEarlyData: int
          browserForwarding: bool
          earlyDataHeader: string
          headers: Dictionary<string, string> option }

    let createWebSocketObject
        (
            path: string option,
            maxEarlyData: int option,
            browserForwarding: bool option,
            earlyDataHeader: string option,
            headers: Dictionary<string, string> option
        ) =
        { WebSocketObject.path =
              match path with
              | None -> "/"
              | Some path -> path
          maxEarlyData =
              match maxEarlyData with
              | None -> 0
              | Some maxEarlyData -> maxEarlyData
          browserForwarding =
              match browserForwarding with
              | None -> false
              | Some browserForwarding -> browserForwarding
          earlyDataHeader =
              match earlyDataHeader with
              | None -> ""
              | Some earlyDataHeader -> earlyDataHeader
          headers = headers }

    type HttpRequestObject =
        { version: string
          method: string
          path: string list
          headers: Dictionary<string, string list> }

    type HttpResponseObject =
        { version: string
          status: string
          reason: string
          headers: Dictionary<string, string list> }

    type TcpHeaderObject =
        { [<JsonField("type")>]
          headerType: string
          request: HttpRequestObject
          response: HttpResponseObject }

    type TcpObject = { header: TcpHeaderObject }

    [<RequireQualifiedAccess>]
    type KcpHeaders =
        | None
        | SRTP
        | UTP
        | WechatVideo
        | DTLS
        | WireGuard

    type UdpHeaderObject =
        { [<JsonField("type")>]
          headerType: KcpHeaders }

    type KcpObject =
        { mtu: int
          tti: int
          uplinkCapacity: int
          downlinkCapacity: int
          congestion: bool
          readBufferSize: int
          writeBufferSize: int
          header: UdpHeaderObject
          seed: string }

    [<RequireQualifiedAccess>]
    type HTTPMethod =
        | GET
        | HEAD
        | POST
        | PUT
        | DELETE
        | CONNECT
        | OPTIONS
        | TRACE
        | PATCH

    type HttpObject =
        { host: string list
          path: string
          method: HTTPMethod
          headers: Dictionary<string, string list> }

    // reserved for future annotations.
    [<RequireQualifiedAccess>]
    type QuicSecurity =
        | None
        | [<JsonField("aes-128-gcm")>] AES
        | [<JsonField("chacha20-poly1305")>] ChaCha20

    type QuicObject =
        { security: string
          key: string
          header: UdpHeaderObject }

    type GrpcObject = { serviceName: string }

    type TLSObject =
        { serverName: string
          allowInsecure: bool
          alpn: string list
          disableSystemRoot: bool }

    [<RequireQualifiedAccess>]
    type TProxyType =
        | Redirect
        | TProxy
        | Off

    type SockoptObject =
        { mark: int
          tcpFastOpen: bool option
          tproxy: TProxyType }

    type StreamSettingsObject =
        { network: Networks
          tls: TLSObject option
          tcp: TcpObject option
          kcp: KcpObject option
          ws: WebSocketObject option
          http: HttpObject option
          quic: QuicObject option
          grpc: GrpcObject option
          sockopt: SockoptObject option }
