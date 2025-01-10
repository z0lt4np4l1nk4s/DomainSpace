import NavigationLink from "./NavigationLink";
import { PageRouteEnum } from "../../types";
import { Link } from "react-router-dom";

export default function DefaultNavBar() {
  return (
    <nav
      className="navbar navbar-expand-lg navbar-dark bg-primary"
      data-bs-theme="dark"
    >
      <div className="container container-fluid">
        <Link className="navbar-brand" to={PageRouteEnum.Home}>
          <img
            src="/logo32.png"
            alt="logo"
            width="32"
            height="32"
            className="d-inline-block align-text-top"
          />{" "}
          DomainSpace
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div
          className="collapse navbar-collapse justify-content-end"
          id="navbarNav"
        >
          <ul className="navbar-nav ms-auto">
            <NavigationLink path={PageRouteEnum.Login}>Login</NavigationLink>
          </ul>
        </div>
      </div>
    </nav>
  );
}
