#r "nuget: QRCoder"

open QRCoder

let writeFile (outputPath: string) (qrCode: string) : unit =
  System.IO.File.WriteAllText(outputPath, qrCode)
  printfn $"QR code saved to %s{outputPath}" 

let generateWifiQrCode (ssid: string) (password: string) (encryption: string) (outputPath: string) =
  let wifiString = 
    sprintf "WIFI:T:%s;S:%s;P:%s;;" 
      (encryption.ToUpper()) 
      (ssid.Replace(";", "\\;")) 
      (password.Replace(";", "\\;"))

  use qrGenerator = new QRCodeGenerator()
  let qrData = qrGenerator.CreateQrCode(wifiString, QRCodeGenerator.ECCLevel.Q)
  use qrCode = new SvgQRCode(qrData)
  
  qrCode.GetGraphic 20
  |> writeFile outputPath

// Example usage
let ssid = "my_ssid"
let password = "my_password"
let encryption = "WPA" // Options: "WPA", "WEP", "nopass"
let outputPath = "C:\\wifi-qr-code.svg"

generateWifiQrCode ssid password encryption outputPath
