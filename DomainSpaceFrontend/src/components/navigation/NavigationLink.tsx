import React, { ReactNode } from "react";
import { Link, useLocation } from "react-router-dom";

export default function NavigationLink({
  path,
  children,
  onClick,
}: {
  path: string;
  children: ReactNode;
  onClick?: () => void;
}) {
  const location = useLocation();

  const isActive = () => {
    return location.pathname === path || location.pathname === path + "/";
  };

  const linkClass = isActive() ? "nav-link active" : "nav-link";

  return (
    <li className="nav-item">
      <Link className={linkClass} to={path} onClick={onClick}>
        {children}
      </Link>
    </li>
  );
}
