using Newtonsoft.Json;

namespace Swindler.Game.Structures
{
	public class Item
	{
		
		public ushort Id { get; }
		public string Name { get; }
		public ushort StackSize { get; }
		public bool IsStackable { get; }

		public Item(ushort id)
		{
			Item i = Config.Items[id];
			
			Id = i.Id;
			Name = i.Name;
			StackSize = i.StackSize;
			IsStackable = i.IsStackable;
		}
		
		
		[JsonConstructor]
		public Item(ushort id, string name, ushort stackSize, bool isStackable)
		{
			Id = id;
			Name = name;
			StackSize = stackSize;
			IsStackable = isStackable;
		}

		public override string ToString()
		{
			return $"{{ItemStack: {Name} ({Id}) x{StackSize} Stackable ? {IsStackable}}}";
		}
	}
}