namespace Aurum.Configurator.Transport

open System.Collections.Generic
open FSharp.Json

type Networks =
    | WS
    | GRPC
    | TCP
    | KCP
    | DomainSocket
    | HTTP
    | QUIC


type Security =
    | None
    | TLS
    | XTLS

type WebSocketObject =
    { path: string option
      maxEarlyData: int option
      browserForwarding: bool option
      earlyDataHeader: string option
      headers: Dictionary<string, string> option }

type HttpRequestObject =
    { version: string option
      method: string option
      path: string list option
      headers: Dictionary<string, string list> option }

type HttpResponseObject =
    { version: string option
      status: string option
      reason: string option
      headers: Dictionary<string, string list> option }

type TcpHeaderObject =
    { [<JsonField("type")>]
      headerType: string option
      request: HttpRequestObject option
      response: HttpResponseObject option }

type TcpObject = { header: TcpHeaderObject option }

type KcpHeaders =
    | None
    | SRTP
    | UTP
    | WechatVideo
    | DTLS
    | WireGuard

type UdpHeaderObject =
    { [<JsonField("type")>]
      headerType: KcpHeaders option }

type KcpObject =
    { mtu: int option
      tti: int option
      uplinkCapacity: int option
      downlinkCapacity: int option
      congestion: bool option
      readBufferSize: int option
      writeBufferSize: int option
      header: UdpHeaderObject option
      seed: string option }

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
    { host: string list option
      path: string option
      method: HTTPMethod option
      headers: Dictionary<string, string list> option }

// reserved for future annotations.
type QuicSecurity =
    | None
    | [<JsonField("aes-128-gcm")>] AES
    | [<JsonField("chacha20-poly1305")>] ChaCha20

type QuicObject =
    { security: string option
      key: string option
      header: UdpHeaderObject option }

type GrpcObject = { serviceName: string option }

type TLSObject =
    { serverName: string option
      allowInsecure: bool option
      alpn: string list option
      disableSystemRoot: bool option }

type TProxyType =
    | Redirect
    | TProxy
    | Off

type SockoptObject =
    { mark: int option
      tcpFastOpen: bool option
      tproxy: TProxyType option }

type StreamSettingsObject =
    { network: Networks option
      tls: TLSObject option
      tcp: TcpObject option
      kcp: KcpObject option
      ws: WebSocketObject option
      http: HttpObject option
      quic: QuicObject option
      grpc: GrpcObject option
      sockopt: SockoptObject option }
