{
  description = "Dev environment with Python 3.10 and .NET 9";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
  };

  outputs = {
    self,
    nixpkgs,
  }: let
    system = "x86_64-linux"; # change to aarch64-linux or x86_64-darwin if needed
    pkgs = import nixpkgs {
      inherit system;
    };
  in {
    devShells.${system}.default = pkgs.mkShell {
      buildInputs = with pkgs; [
        dotnet-sdk_9
      ];

      shellHook = ''
        echo "Dev environment loaded:"
        echo "- Python: $(python --version)"
        echo "- .NET: $(dotnet --version)"
      '';
    };
  };
}
