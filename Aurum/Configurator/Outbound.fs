namespace Aurum.Configurator.Outbound

open FSharp.Json
open Aurum.Configurator.Transport

// reserved for future annotations.
type ShadowsocksEncryption =
    | None
    | Plain
    | [<JsonField("chacha20-poly1305")>] ChaCha20
    | [<JsonField("chacha20-ietf-poly1305")>] ChaCha20IETF
    | [<JsonField("aes-128-gcm")>] AES128
    | [<JsonField("aes-256-gcm")>] AES256

// VLESS is removed in v2ray-go, so this is subject to removal too (may happen in any time).
type VLESSEncryption = | None

type Protocols =
    | VLESS // subject to removal.
    | VMess
    | Shadowsocks
    | Trojan

// reserved for future annotations.
type VMessEncryption =
    | None
    | Zero
    | Auto
    | [<JsonField("aes-128-gcm")>] AES
    | [<JsonField("chacha20-poly1305")>] ChaCha20

type UserObject =
    { id: string option
      encryption: VLESSEncryption option
      level: int option
      alterId: int option
      security: string option (* should be VMessEncryption but not supported by the Serializer/Deserializer library *)  }

// v2ray-go specific implementation, removed VLESS components.
type GoUserObject =
    { id: string option
      level: int option
      alterId: int option
      security: string option }

type ServerObject =
    { address: string option
      port: int option
      password: string option
      email: string option
      level: int option
      method: string option (* should be ShadowsocksEncryption but not supported by the Serializer/Deserializer library *)
      ivCheck: bool option
      users: UserObject list option }

type OutboundConfigurationObject =
    { vnext: ServerObject list option
      servers: ServerObject list option }

// v2ray-go specific implementation, removed vnext layer.
type GoOutboundConfigurationObject =
    { address: string option
      port: int option
      users: UserObject list option
      servers: ServerObject list option }

type MuxObject = { enabled: bool; concurrency: int }

type GenericOutboundObject<'T> =
    { sendThrough: string option
      protocol: Protocols option
      settings: 'T option
      tag: string option
      streamSettings: StreamSettingsObject option
      mux: MuxObject option }

type OutboundObject = GenericOutboundObject<OutboundConfigurationObject>
// v2ray-go specific implementation, removed vnext and VLESS.
type GoOutboundObject = GenericOutboundObject<GoOutboundConfigurationObject>
