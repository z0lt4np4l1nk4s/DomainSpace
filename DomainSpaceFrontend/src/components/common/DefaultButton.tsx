import { MouseEventHandler } from "react";

export default function DefaultButton({
  onClick,
  type = "button",
  color = "primary",
  size,
  width,
  children,
  disabled,
  isLoading,
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
  isLoading?: boolean;
}) {
  return (
    <div className="d-flex align-items-center" style={{ height: "36px" }}>
      <button
        type={type}
        className={`btn btn-${color} ${size ? `btn-${size}` : ""} ${
          width ? "" : "w-100"
        }`}
        onClick={(e) => {
          if (!isLoading && onClick) {
            onClick(e);
          }
        }}
        disabled={disabled || isLoading}
        style={{ width: width ? `${width}px` : undefined }}
      >
        {isLoading ? (
          <span
            className="spinner-border spinner-border-sm text-light"
            role="status"
            style={{ width: "1.2rem", height: "1.2rem" }}
            aria-hidden="true"
          ></span>
        ) : (
          children
        )}
      </button>
    </div>
  );
}
