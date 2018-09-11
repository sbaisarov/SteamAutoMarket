namespace SteamAutoMarket.CustomElements.Utils
{
    using System.Windows.Forms;

    public class CellProcessor<TCell, TValue>
        where TCell : DataGridViewCell
    {
        public CellProcessor(DataGridViewCell cell)
        {
            this.Cell = (TCell)cell;
            this.Value = CellGetter.GetCellValue<TValue>(cell);
        }

        public TCell Cell { get; private set; }

        public TValue Value { get; private set; }

        public void ChangeCellValue(TValue value)
        {
            this.Cell.Value = value;
        }
    }
}