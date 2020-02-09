namespace Mine.Models
{
    /// <summary>
    /// Item for the Game
    /// </summary>
    public class ItemModel : BaseModel
    {
        // Add Unique attributes for Item

        // The Value of the Item
        public int Value { get; set; } = 0;

        public ItemModel()
        {

        }

        // Copy constructor
        public ItemModel(ItemModel data)
        {
            Update(data);
        }

        public bool Update(ItemModel data)
        {
            //update the database
            Name = data.Name;
            Description = data.Description;

            //update extend
            Value = data.Value;

            return true;
        }
    }
}