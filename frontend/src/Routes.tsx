import { createBrowserRouter, RouteObject } from "react-router";
import App from "./App";
import HomePage from "./pages/HomePage";
import TripPage from "./pages/TripPage";

export const routes: RouteObject[] = [
    {
        path : "",
        element : <App/>,
        children: [
            {
                path: "",
                element: <HomePage/>
            },
            {
                path : "trip/:tripId",
                element: <TripPage/>
            }
        ]
    }
]

export const router = createBrowserRouter(routes);
