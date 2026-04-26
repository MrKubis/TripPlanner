export interface PaginatedResult<T> {
    data: T[],
    count: number,
    pageSize: number,
    pageIndex: number
}

export interface TripShort{
    id: string,
    title: string,
    createdBy: string,
    //Date
    createdOn: string
}

export interface Trip{
    id: string,
    title: string,
    createdBy: string
    //Date
    createdOn: string,
    description: string,
    destinations: Destination[],
    links: Link[],
    days: Day[],
}
export interface Day{
    id: string,
    date: Date,
    destinationIds: string[]
}
export interface Destination {
    id:string,
    name: string,
    location: Location,
    linkIds: string[]
}
export interface Link {
    id: string,
    url: string,
    title: string
}
export interface Location{
    latitude: number,
    longitude: number
}