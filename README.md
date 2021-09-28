# Excel To Json Extractor

A program to read .xls trading data that contains stock trading actions. The file is read utilizing [ExcelReadFactory](https://github.com/ExcelDataReader/ExcelDataReader).  
The file is originally acquired from [Nordea](https://www.nordea.fi/) Investor stock trading application. This requires one to have an account and perform investment actions on the site.

Program parses the .xls file and groups the result by the `Name` column of the .xls file.
The data is then saved to .json format. A single file contains all trading data for one `Name` identifier.

For example trade information for **Company X**:

    "Id": 0,                    // Auto generated by the program, incremented with each json file
    "Name": "Company X",
    "CurrentQuantity": 1,       // Current amount of stocks invested in
    "TotalBuyAmount": 10,       // Total amount of stocks bought
    "TotalSellAmount": 20,      // Total amount of stocks sold
    "TotalDividendAmount": 12,  // Total amount of dividend
    "Currency": "EUR",
    "Buys": [
      {
        "RecordNumber": "000000000001",
        "Quantity": 2,
        "DateOfAction": "01.01.2021",
        "Rate": 5.00,                   // The market value of a single stock at the time of purchase
        "TotalTransactionAmount": 10
      },
    "Sells": [
      {
        "RecordNumber": "000000000002",
        "Quantity": 1,
        "DateOfAction": "02.02.2021",
        "Rate": 20,
        "TotalTransactionAmount": 20
      }],
    "Dividends": [
      {
        "PaymentDate": "Mon Feb 01 00:00:00 CEST 2021",
        "TypeOfAction": "A Dividend",
        "TotalTransactionAmount": 12.00
      },
      
Note: Things like fees are included in `TotalTransactionAmount` variable but not visible otherwise. 
