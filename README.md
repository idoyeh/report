# report
Wpf application for manager: C#, WPF, MSSQL.
Read and enter data and run complex queries(Dynamic SQL).

Develop an exceptions report:
Create a gui user interface that can be exported with a report that reports exceptions or value equality to a particular value by checking the database.


Report structure:
The user selects the report period(start date and end date), a field for testing, operator and value.

- Before viewing the report, it is checked to see if the corresponding data is valid(According to the status) and view the results of the report in a table, if the data is invalid, its status must be displayed instead(Name).

- Filters by the selected field and displays all columns in the same timestamp. If no date is selected, the entire date range in the report will be displayed.

# Example
![report](https://user-images.githubusercontent.com/64954264/127176227-fb3c1fdc-ea8a-472f-8c55-c453ce876595.gif)



The sql database has 3 tables: DATA, STATUS and OPERATOR.

1. DATA - A table that contains the values according to the different fields and the timestamp of each value.
Time_Date - The timestamp. Uses the "From Date" and "To Date" fields.
Value# - Measurement values. Uses the field selection menu in the report.
Status# - Work mode. Used to check if the data is valid.
Fields Value and Status are linked to each other by the number.

2. OPERATOR - A table that contains the different operators.

3. STATUS - A table containing the different statutes.
Valid - Where the column value is 1, the data is correct.
If the value is 0 , the Name must be displayed instead of the value in the report results.

![‏‏reportImg](https://user-images.githubusercontent.com/64954264/127173583-cc25a721-e27a-45e4-8625-30ec85aae4fc.PNG)

