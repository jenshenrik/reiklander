{
  description = "Dotnet development environment";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs {
          inherit system;
        };

        dotnetSdk = pkgs.dotnet-sdk_10; # change version if needed
      in
      {
        devShells.default = pkgs.mkShell {
          buildInputs = [
            dotnetSdk
            pkgs.git
            pkgs.zlib
            pkgs.openssl
            pkgs.icu
          ];

          shellHook = ''
            export DOTNET_ROOT=${dotnetSdk}
            export PATH=$DOTNET_ROOT/bin:$PATH
            echo "🚀 .NET dev shell ready"
          '';
        };
      });
}