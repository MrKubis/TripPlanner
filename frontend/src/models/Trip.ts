import { Day } from "./Day"
import { Destination } from "./Destination"
import { Expense } from "./Expense"
import { Link } from "./Link"

export type Trip = {
    Id:             string,
    Title?:         string,
    CreatedBy:      string,
    CreatedOn:      Date,
    Destinations:   Destination[]
    Links:          Link[],
    Days:           Day[],
    Expenses:       Expense[]
}
