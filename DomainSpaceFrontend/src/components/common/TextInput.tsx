import { useEffect, useState } from "react";

export default function TextInput({
  title = undefined,
  type = "text",
  name,
  value,
  onChange,
  width,
  minValue,
  maxValue,
  onValidate,
  onValidationChange,
  disabled = false,
}: {
  title: string;
  type: "password" | "text" | "email" | "number";
  name?: string;
  value: string;
  unit?: string;
  minValue?: number;
  maxValue?: number;
  onChange: React.ChangeEventHandler<HTMLInputElement>;
  onValidate?: (value: string) => string;
  onValidationChange?: (name: string, isValid: boolean) => void;
  disabled?: boolean;
  width: number;
} & any) {
  const [isTouched, setIsTouched] = useState(false);
  const [inputValue, setInputValue] = useState(value || "");
  const [isValid, setIsValid] = useState(true);
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    if (!onValidate) return;
    const error = onValidate(inputValue) || undefined;
    const isInputValid = !error;

    setIsValid(isInputValid);
    setErrorMessage(error || "Field required");
    if (onValidationChange) {
      onValidationChange(name, isInputValid);
    }
  }, [inputValue]);

  return (
    <div className="form-group mb-3 mt-3" style={{ maxWidth: width }}>
      {title && (
        <div className="mb-1">
          <label htmlFor={name} className="form-label">
            {title}
          </label>
        </div>
      )}
      <div className="">
        <input
          className={`form-control ${
            isTouched && !isValid ? "is-invalid" : ""
          }`}
          type={type}
          id={name}
          name={name}
          value={value || ""}
          onFocus={() => {
            if (!isTouched) {
              setIsTouched(true);
            }
          }}
          onChange={(event) => {
            if (!isTouched) {
              setIsTouched(true);
            }
            setInputValue(event.target.value);
            onChange(event);
          }}
          min={minValue}
          max={maxValue}
          disabled={disabled}
        />
        {isTouched && (
          <div className="invalid-feedback text-danger">{errorMessage}</div>
        )}
      </div>
    </div>
  );
}
