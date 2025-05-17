using Avalonia.Controls;
using Avalonia.Input;
using SHA3.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace TextHasher.Views;

public static class ByteArrayExtensions
{
    public static string ToHexString(this byte[] bytes)
    {
        StringBuilder sb = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
            sb.AppendFormat("{0:x2}", b);
        return sb.ToString();
    }
}

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string input = TextToEncrypt.Text;
        string hashResult = string.Empty;

        switch (EncryptionList.SelectedIndex)
        {
            case 0: // MD5
                using (MD5 md5 = MD5.Create())
                {
                    hashResult = ComputeHash(md5, input);
                }
                break;

            case 1: // SHA256
                using (SHA256 sha256 = SHA256.Create())
                {
                    hashResult = ComputeHash(sha256, input);
                }
                break;

            case 2: // SHA-1
                using (SHA1 sha1 = SHA1.Create())
                {
                    hashResult = ComputeHash(sha1, input);
                }
                break;

            case 3: // SHA-3
                hashResult = Sha3.Sha3256().ComputeHash(Encoding.UTF8.GetBytes(input)).ToHexString();
                break;

            default:
                hashResult = "Select a valid algorithm";
                break;
        }

        EncryptedText.Text = hashResult;
    }

    private string ComputeHash(HashAlgorithm algorithm, string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = algorithm.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    private void GitHubLink_Click(object sender, PointerPressedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/CinnamonYeti459",
            UseShellExecute = true
        });
    }
}
