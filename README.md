# Unity Demo HyperPlay

## Connect

![](https://user-images.githubusercontent.com/19412160/203439364-21433f11-6197-49df-84cd-c3b8fe355f8b.png)

![](https://user-images.githubusercontent.com/19412160/203439377-0a34be6e-b738-407c-858a-547909779d6d.png)

![](https://user-images.githubusercontent.com/19412160/203439946-cc7f9138-bba5-4904-9330-9f8eb9ce6f0c.jpeg)

## Start Unity Project

![](https://user-images.githubusercontent.com/19412160/203439551-cd6347e2-e245-4c8c-8560-c6a2187b754f.png)

- Sync Balance: Uses web3.unity to fetch users balance. If the user has a token, then change character to red
  - Logic found in `/Assets/HYPERPLAY/SyncBalance.cs`
- Mint Token: Connects to metamask wallet to mint a token
  - Logic found in `/Assets/HYPERPLAY/MintToken.cs`
- Burn Tokens: Connects to metamask wallet to burn all of a players token. (a way to reset)
  - Logic found in `/Assets/HYPERPLAY/BurnToken.cs`

## Solidity

https://goerli.etherscan.io/address/0xDD6ff2bA7fD02D19e8e4e1d99b65802eD9705437

```Solidity
// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";

contract ERC20Token is ERC20 {
    constructor() ERC20("Test Token20", "tTKN20") {}

    // mints 1 token
    function publicMint() public {
        _mint(msg.sender, 1);
    }

    // burns all of users token
    function publicBurn() public {
        _burn(msg.sender, this.balanceOf(msg.sender));
    }
}
```

