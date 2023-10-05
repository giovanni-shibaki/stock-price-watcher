# Stock Price Watcher

## Overview

Stock Price Watcher is a C# command-line application designed to monitor stock prices on the B3 stock exchange (The primary financial exchange in Brazil) and notify users via email when the price of a specified asset falls below a certain threshold for buying or rises above another threshold for selling. This console-based tool operates without a graphical interface and provides a convenient way to stay informed about market conditions.

## Configuration

Before using Stock Price Watcher, you need to set up a configuration file in JSON format with the name **appsettings.json** on the same folder as the **StockPriceWatcher.exe** file. This file should include the following information:

- A list of destination email addresses for alerts.
- SMTP server access credentials for sending emails.
- API key for accessing stock quotes.
- Update delay time (in milliseconds) to specify how often the system checks for price changes and sends email alerts.

Here's an example of the configuration file:

```json
{
    "emails": [
        "user1@example.com",
        "user2@example.com"
    ],
    "SMTPSettings": {
        "Server": "smtp.example.com",
        "Port": 587,
        "Username": "your_username",
        "Password": "your_password"
    },
    "APIToken": "your_api_key",
    "updateDelay": 1800000
}
```

The API used on this project is provided by [Brapi](https://brapi.dev/) using their free licence. Therefore, the Deserialization for the response JSON file follows their API structure.

For the SMTP server, this project uses a free testing SMTP server provided by [MailTrap](https://mailtrap.io/) using their free licence.

## Usage

To use Stock Price Watcher, follow these steps:

1. Clone this repository to your local machine.

2. Build the application using your C# compiler.

3. Create the JSON configuration file (as shown above) and save it as `appsettings.json` in the project directory (the same one of the executable file).

4. Open a terminal or command prompt and navigate to the project directory.

5. Run the application with the following command, providing three command-line arguments:

   - The first argument is the symbol of the asset to monitor (e.g., PETR4).
   - The second argument is the reference price for selling.
   - The third argument is the reference price for buying.

   Example usage:

   ```shell
   .\StockPriceWatcher.exe PETR4 35 33,3
   ```

6. Stock Price Watcher will continuously monitor the stock's market price using the provided API key and update delay. When the price falls below the buying threshold or rises above the selling threshold, the system will send email alerts to the specified recipients.

## Observer Design Pattern

Stock Price Watcher utilizes the Observer design pattern to handle its functionality. In this pattern:

- The **StockMonitor** acts as the Observable class, periodically (following the defined updateDelay) calls for the API and get the Regular Market Price for the specified stock Symbol, and notifying the subscribed Observers if necessary.
- The **StockReporter** acts as the Observer class, receiving notifications from the StockMonitor and calling the **MailHandler** class to send an email to the registered email addresses.
-  When price changes occur, the Observable (StockMonitor) notifies its observers (StockReporter) automatically.

This design pattern allows for a decoupled and extensible system, where additional observers can be easily added without modifying the core logic.
