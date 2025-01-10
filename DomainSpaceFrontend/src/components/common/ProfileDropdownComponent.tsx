import { useState } from "react";
import { PageRouteEnum } from "../../types";
import { NavigationLink } from "../navigation";
import { AuthService, UserDataService } from "../../services";
import { ToastUtil } from "../../utils";
import { useNavigate } from "react-router-dom";

const authService = new AuthService();

export default function ProfileDropdownComponent() {
  const navigate = useNavigate();
  const [isOpen, setIsOpen] = useState(false);

  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };

  return (
    <div className="nav-item dropdown">
      <a
        href="#"
        className="nav-link dropdown-toggle d-flex align-items-center"
        onClick={toggleDropdown}
        id="profileDropdown"
        role="button"
        data-bs-toggle="dropdown"
        aria-expanded={isOpen ? "true" : "false"}
      >
        <span className="ms-2">{UserDataService.getUserToken()?.email}</span>
      </a>
      <ul
        className={`dropdown-menu dropdown-menu-end ${
          isOpen ? "show" : ""
        } bg-primary shadow`}
        aria-labelledby="profileDropdown"
      >
        <NavigationLink path={PageRouteEnum.Account}>
          {"Account"}
        </NavigationLink>
        <li className="nav-item">
          <a
            className={"nav-link text-danger"}
            style={{
              cursor: "pointer",
            }}
            onClick={async () => {
              const result = await authService.logoutAsync();
              if (result.isSuccess) {
                navigate(PageRouteEnum.Login);
              } else {
                ToastUtil.showErrorMessage("Failed to logout.");
              }
            }}
          >
            {"Logout"}
          </a>
        </li>
      </ul>
    </div>
  );
}
