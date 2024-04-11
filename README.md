# ShopUtils

Before use, you need to download [ContentWarningUnityTemplate](https://github.com/hyydsz/ContentWarningUnityTemplate)

## Support API
- Items API
- ItemSpawn API
- ItemDataEntry API
- Language API
- Network API

## How To Use
### Plugin
```csharp
[BepInPlugin(ModGUID, ModName, ModVersion)]
[BepInDependency("hyydsz-ShopUtils")]
public class Example : BaseUnityPlugin {

}
```

### Items API
```csharp
void Awake() {
    // RegisterShopItem(Item item, ShopItemCategory category = ShopItemCategory.Invalid, int price = -1)
    Items.RegisterShopItem(Item: item);
}
```

### ItemSpawn API
If you want your item to random in the map: 
```csharp
void Awake() {
    // RegisterSpawnableItem(Item item, Item.RARITY Rarity = Item.RARITY.common, int BudgetCost = 1)
    Items.RegisterSpawnableItem(Item: item);

    // Beta
    // RegisterSpawnableArtifactItem(Item item, float Rarity = 1, int BudgetCost = 1)
    Items.RegisterSpawnableArtifactItem(Item: item);
}
```

### ItemDataEntry API
if your item need to save some `data`

Example:
```csharp
public class ExampleEntry : ItemDataEntry, IHaveUIData
{
    public int example = 0;

    public override void Deserialize(BinaryDeserializer binaryDeserializer)
    {
        example = binaryDeserializer.ReadInt();
    }

    public override void Serialize(BinarySerializer binarySerializer)
    {
        binarySerializer.WriteInt(example);
    }

    public string GetString()
    {
        return "UI String";
    }
}
```

In Awake:
```csharp
void Awake() {
    Entries.RegisterAll();
    // or
    Entries.RegisterEntry(typeof(ExampleEntry));
}
```

### Language API
Available Languages:
- Chinese (Simplified) (zh-Hans)
- Chinese (Traditional) (zh-Hant)
- English (en)
- French (fr)
- German (de)
- Italian (it)
- Japanese (ja)
- Portuguese (Brazil) (pt-BR)
- Russian (ru)
- Spanish (es)
- Ukrainian (uk)
- Korean (ko)
- Swedish (sv)

Example:
```csharp
void Awake() {
    // If your item name is Test
    // Splite by ';'

    Locale Chinese = Languages.GetLanguage("zh-Hans");
    Languages.AddLanguage("Test-ToolTips", "[LMB] Use;[RMB] Aim", Chinese); // ToolTips
    Languages.AddLanguage("Test", "Name is Test", Chinese); // Item DisplayName
}
```

### Network API
You can use [MyceliumNetworking](https://github.com/RugbugRedfern/Mycelium-Networking-For-Content-Warning) instead

Example:
```csharp
void Awake() {
    // Everyone can synchronize price
    Networks.RegisterItemPrice(Item: item);

    Networks.ObLobbyCreated += () => {
        // If you are host. you can set lobby data here
        Networks.SetLobbyData(string: key, string: data)
    };

    Networks.OnLobbyEnter += () => {
        // If you are client. you can get lobby data here
        string data = Networks.GetLobbyData(string: key)
    };
}
```

### Debug
- Set the money to 99999
```csharp
void Awake() {
    Shops.DebugMode();
}
```

## Fork & Clone
Need Following DLL:
- Assembly-CSharp-nstrip.dll [NStrip](https://github.com/bbepis/NStrip?tab=readme-ov-file)
- Zorro.Core.Runtime.dll
- Sirenix.Serialization.dll