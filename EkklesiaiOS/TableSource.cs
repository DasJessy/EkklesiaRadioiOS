using System;
using Foundation;
using UIKit;

namespace EkklesiaiOS
{
    public class TableSource : UITableViewSource
    {

        public string[] TableItems;
        string CellIdentifier = "TableCell";


        public TableSource(string[] items)
        {
            TableItems = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            string item = TableItems[indexPath.Row];


            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
                cell.DetailTextLabel.Lines = 0;
                cell.DetailTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
            }

            var splitted = item.Split('^');
            var msg = "";
            for (int i = 1; i < splitted.Length; i++)
            {
                msg += splitted[i];
            }

            var userToDisplay = splitted[0];

            cell.TextLabel.Text = "" + userToDisplay;
            cell.TextLabel.Font = UIFont.FromName("Cochin-BoldItalic", 16f);
            cell.TextLabel.TextColor = UIColor.FromRGB(252, 127, 8);

            cell.DetailTextLabel.Text = msg;
            cell.DetailTextLabel.Font = UIFont.SystemFontOfSize(16f);

            return cell;

        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Length;
        }
    }
}
