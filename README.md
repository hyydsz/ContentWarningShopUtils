# ShopUtils

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