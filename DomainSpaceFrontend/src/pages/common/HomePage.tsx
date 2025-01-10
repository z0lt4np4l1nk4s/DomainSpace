import { LandingPage } from ".";
import { UserDataService } from "../../services";
import { ContentPage } from "../content";

export default function HomePage() {
  if (!UserDataService.isAuthenticated()) {
    return <LandingPage />;
  }

  return <ContentPage />;
}
