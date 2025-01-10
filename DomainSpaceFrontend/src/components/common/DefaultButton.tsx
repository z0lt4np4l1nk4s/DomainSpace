import { MouseEventHandler } from "react";

export default function DefaultButton({
  onClick,
  type = "button",
  color = "primary",
  size,
  width,
  children,
  disabled,
}: {
  onClick?: MouseEventHandler<HTMLButtonElement>;
  type?: "submit" | "reset" | "button";
  color?:
    | "primary"
    | "secondary"
    | "success"
    | "danger"
    | "warning"
    | "info"
    | "light"
    | "dark";
  size?: "sm" | "lg";
  width?: number;
  children?: React.ReactNode;
  disabled?: boolean;
}) {
  return (
    <div className="d-flex align-items-center" style={{ height: "36px" }}>
      <button
        type={type}
        className={`btn btn-${color} ${size ? `btn-${size}` : ""} ${
          width ? "" : "w-100"
        }`}
        onClick={onClick}
        disabled={disabled}
        style={{ width: width ? `${width}px` : undefined }}
      >
        {children}
      </button>
    </div>
  );
}
