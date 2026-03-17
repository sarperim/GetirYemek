namespace Catalog.Domain.Entities
{
    public class ProductModifier
    {
        public Guid Id { get; set; }
        public Guid FoodId { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public int MinSelection { get; set; }
        public int MaxSelection { get; set; }

        private readonly List<ModifierItem> _modifierItems = new();
        public IReadOnlyCollection<ModifierItem> ModifierItems => _modifierItems.AsReadOnly();

        public void AddModifierEvent(ModifierItem modifierItem)
        {
            _modifierItems.Add(modifierItem);
        }

    }
}
