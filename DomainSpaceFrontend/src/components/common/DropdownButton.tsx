import React from "react";
import { DropdownButtonModel } from "../../types";

export default function DropdownButton({
  items,
  children,
}: {
  items: DropdownButtonModel[];
  children: React.ReactNode;
}) {
  return (
    <div className="dropdown">
      <button
        className="btn btn-link"
        id="dropdownMenuButton"
        data-bs-toggle="dropdown"
        aria-expanded="false"
      >
        {children}
      </button>
      <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton">
        {items.map((item, index) => (
          <li key={index}>
            <button
              className="dropdown-item"
              onClick={item.onClick}
              type="button"
            >
              {item.text}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
