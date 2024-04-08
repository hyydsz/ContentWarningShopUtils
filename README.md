# ShopUtils

Before use, you need to download [ContentWarningUnityTemplate](https://github.com/hyydsz/ContentWarningUnityTemplate)

## Support API
- Items API
- ItemDataEntry API

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
    Items.RegisterItem(Item: item);
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

### Debug
- Set all item prices to 0
```csharp
void Awake() {
    Shops.DebugMode();
}
```

## Fork & Clone
Need Following DLL:
- Assembly-CSharp-nstrip.dll
- Zorro.Core.Runtime.dll
- Sirenix.Serialization.dll