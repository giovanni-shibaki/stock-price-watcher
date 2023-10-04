# Stock Price Watcher

## Overview

Stock Price Watcher is a command-line application designed to monitor stock prices on the B3 (The primary financial exchange in Brazil) stock exchange and notify users via email when the price of a specified asset falls below a certain threshold for selling or rises above another threshold for buying. This console-based tool operates without a graphical interface and provides a convenient way to stay informed about market conditions.

## Configuration

Before using Stock Price Watcher, you need to set up a configuration file in JSON format. This file should include the following information:

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

## Usage

To use Stock Price Watcher, follow these steps:

1. Clone this repository to your local machine.

2. Build the application using your C# compiler.

3. Create the JSON configuration file (as shown above) and save it as `config.json` in the project directory.

4. Open a terminal or command prompt and navigate to the project directory.

5. Run the application with the following command, providing three command-line arguments:

   - The first argument is the symbol of the asset to monitor (e.g., AAPL for Apple Inc.).
   - The second argument is the reference price for selling.
   - The third argument is the reference price for buying.

   Example usage:

   ```shell
   .\StockPriceWatcher.exe PETR4 35 33,3
   ```

6. Stock Price Watcher will continuously monitor the stock's market price using the provided API key and update delay. When the price falls below the selling threshold or rises above the buying threshold, the system will send email alerts to the specified recipients.

## Observer Design Pattern

Stock Price Watcher utilizes the Observer design pattern to handle its functionality. In this pattern:

- The stock price data is the subject being observed.
- The email notification system acts as an observer.
- When price changes occur, the subject (stock price) notifies its observers (email notification system) automatically.

This design pattern allows for a decoupled and extensible system, where additional observers (e.g., SMS notifications) can be easily added without modifying the core logic.

## How It Works

Once Stock Price Watcher is initiated with the correct command-line arguments and configuration file, it performs the following steps:

1. Connects to the Brapi API using the provided API key.

2. Periodically checks the market price of the specified asset, as defined by the update delay in the configuration file.

3. Compares the current market price to the predefined buying and selling thresholds.

4. If the current price falls below the selling threshold or rises above the buying threshold, the system sends email alerts to all registered email addresses, suggesting either buying or selling the asset.

5. The system continues to monitor the market and sends alerts whenever the price crosses the specified thresholds.

By following these instructions, you can effectively use Stock Price Watcher to receive timely notifications about potential stock trading opportunities based on your predefined criteria.
