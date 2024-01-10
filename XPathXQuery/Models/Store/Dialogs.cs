namespace XPathXQuery.Models;

public static class Dialogs
{
    public static bool GenerateConfirmation(string message, string title, MessageBoxImage image)
    {
        MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, image);
        switch (result)
        {
            case MessageBoxResult.Yes:
                return true;
            case MessageBoxResult.No:
                return false;
            default:
                return false;
        }
    }

    public static void GenerateMessage(MessageBoxImage type, string message)
    {
        switch (type)
        {
            case MessageBoxImage.Information:
                MessageBox.Show(message, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                break;
            case MessageBoxImage.Warning:
                MessageBox.Show(message, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                break;
            case MessageBoxImage.Error:
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                break;
        }
    }

}
