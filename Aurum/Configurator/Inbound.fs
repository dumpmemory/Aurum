namespace Aurum.Configurator.Inbound

open Aurum.Configurator.Transport

// reserved for future annotations.
type Protocols =
    | HTTP
    | Socks
    | DokodemoDoor

type SocksAuth =
    | NoAuth
    | Password

type AccountObject =
    { user: string option
      pass: string option }

type InboundConfigurationObject =
    { accounts: AccountObject list option
      userLevel: int option
      timeout: int option
      (*HTTP specific*)
      allowTransparent: bool option
      (*SOCKS specific*)
      auth: SocksAuth option
      udp: bool option
      ip: string option
      (*Dokodemo-door specific*)
      address: string option
      port: int option
      network: string option
      followRedirect: bool option }

type SniffingObject =
    { enabled: bool option
      destOverride: string list option
      metadataOnly: bool option }

type InboundObject =
    { listen: string option
      port: int option
      protocol: string option
      settings: InboundConfigurationObject option
      tag: string option
      sniffing: SniffingObject option }
