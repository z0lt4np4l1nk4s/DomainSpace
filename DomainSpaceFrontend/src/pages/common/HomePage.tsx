import { LandingPage } from ".";
import { TokenService } from "../../services";
import { ContentPage } from "../content";

export default function HomePage() {
  if (!TokenService.isAuthenticated()) {
    return <LandingPage />;
  }

  return <ContentPage />;
}
