import { useEffect, useState } from "react";

export default function DateInput({
  title,
  name,
  onChange,
  value,
  width,
  minDate,
  maxDate,
  onValidate,
  onValidationChange,
}: {
  title: string;
  name: string;
  value: Date;
  width: number;
  minDate?: Date;
  maxDate?: Date;
  onChange: React.ChangeEventHandler<HTMLInputElement>;
  onValidate?: (value: string) => string;
  onValidationChange?: (name: string, isValid: boolean) => void;
} & any) {
  const [isTouched, setIsTouched] = useState(false);
  const [inputValue, setInputValue] = useState(value || "");
  const [isValid, setIsValid] = useState(true);
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    const error = onValidate ? onValidate(inputValue) : undefined;
    const isInputValid = !error;

    setIsValid(isInputValid);
    setErrorMessage(error || "Field required");
    if (onValidationChange) {
      onValidationChange(name, isInputValid);
    }
  }, [inputValue, name, onValidationChange]);

  const formatDateForInput = (date: Date) => {
    if (!date) return "";

    const localDate = new Date(date);
    const year = localDate.getFullYear();
    const month = (localDate.getMonth() + 1).toString().padStart(2, "0");
    const day = localDate.getDate().toString().padStart(2, "0");

    return `${year}-${month}-${day}`;
  };

  return (
    <div className="form-group mb-3 mt-3" style={{ maxWidth: width }}>
      {title && (
        <>
          <label htmlFor={name} className="form-label">
            {title}
          </label>
        </>
      )}
      <input
        className={`form-control ${isTouched && !isValid ? "is-invalid" : ""}`}
        type="date"
        id={name}
        name={name}
        min={minDate && formatDateForInput(minDate)}
        max={maxDate && formatDateForInput(maxDate)}
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
        value={formatDateForInput(inputValue)}
      />
      {isTouched && <div className="invalid-feedback">{errorMessage}</div>}
    </div>
  );
}
