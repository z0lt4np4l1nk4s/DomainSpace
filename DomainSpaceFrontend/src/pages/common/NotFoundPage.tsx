import { FooterComponent, NavBar } from "../../components";

export default function NotFoundPage() {
  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <div className="container d-flex align-items-center justify-content-center mt-5">
          <h1>Not found</h1>
        </div>
      </div>
      <FooterComponent />
    </div>
  );
}
