namespace Dorisoy.AMS.models
{
    public class AssetViewModel
    {
        public Asset Asset { get; set; }

        /// <summary>
        /// 可用库存（总数 - 借用数）
        /// </summary>
        public decimal AvailableQuantity { get; set; }
    }
}
