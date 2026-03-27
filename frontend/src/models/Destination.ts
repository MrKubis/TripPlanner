import { Link } from "./Link"
import { Location } from "./Location"
export type Destination = {
    Id :        string,
    Name :      string,
    Location:   Location,
    LinkIds:    string[]
}