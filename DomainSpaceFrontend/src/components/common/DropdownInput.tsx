import { useEffect, useState } from "react";
import { DropdownItemModel } from "../../types";
import ReactSelect, { MultiValue, SingleValue } from "react-select";

export default function DropdownInput({
  title,
  name,
  value,
  options = [],
  multiple = false,
  width,
  defaultErrorMessage,
  onChange,
  onValidationChange,
  validate,
}: {
  title: string;
  name: string;
  value?: DropdownItemModel | DropdownItemModel[];
  options?: DropdownItemModel[];
  multiple?: boolean;
  width?: number;
  defaultErrorMessage?: string;
  onChange?: (
    newValue: MultiValue<DropdownItemModel> | SingleValue<DropdownItemModel>
  ) => void;
  onValidationChange?: (name: string, isValid: boolean) => void;
  validate?: boolean;
}) {
  const [inputValue, setInputValue] = useState<
    MultiValue<DropdownItemModel> | SingleValue<DropdownItemModel>
  >(
    multiple
      ? (value as MultiValue<DropdownItemModel>)
      : (value as SingleValue<DropdownItemModel>)
  );
  const [isTouched, setIsTouched] = useState(false);
  const [isValid, setIsValid] = useState(true);

  useEffect(() => {
    const isFormInvalid =
      validate === false
        ? false
        : !inputValue ||
          (inputValue as MultiValue<DropdownItemModel>).length === 0;
    setIsValid(!isFormInvalid);
    if (onValidationChange) {
      onValidationChange(name, !isFormInvalid);
    }
  }, [inputValue, onValidationChange]);

  return (
    <div className="form-group mb-3 mt-3" style={{ width }}>
      {title && (
        <div className="mb-1">
          <label htmlFor={name} className="form-label">
            {title}
          </label>
        </div>
      )}
      <ReactSelect
        id={name}
        name={name}
        value={value}
        onFocus={() => {
          if (!isTouched) {
            setIsTouched(true);
          }
        }}
        className={`${isTouched && !isValid ? "is-invalid" : ""}`}
        onChange={(value) => {
          if (!isTouched) {
            setIsTouched(true);
          }
          setInputValue(value);
          onChange!(value);
        }}
        isMulti={multiple}
        options={options}
        styles={{
          control: (styles, { isFocused }) => ({
            ...styles,
            backgroundColor: "#f8f9fa",
            borderColor: isTouched && !isValid ? "#dc3545" : "#ced4da",
            borderRadius: "0.375rem",
            boxShadow: isFocused
              ? "0 0 0 0.2rem rgba(13, 110, 253, 0.25)"
              : "none",
            height: "38px",
          }),
          option: (styles, { isFocused, isSelected }) => ({
            ...styles,
            backgroundColor: isSelected
              ? "#0d6efd"
              : isFocused
              ? "#e9ecef"
              : "#ffffff",
            color: isSelected ? "#ffffff" : "#212529",
            cursor: "pointer",
          }),
          input: (styles) => ({
            ...styles,
            color: "#212529",
          }),
          placeholder: (styles) => ({
            ...styles,
            color: "#6c757d",
          }),
          singleValue: (styles) => ({
            ...styles,
            color: "#212529",
          }),
          menu: (styles) => ({
            ...styles,
            backgroundColor: "#ffffff",
            borderRadius: "0.375rem",
            marginTop: "0.1rem",
            boxShadow: "0 0.5rem 1rem rgba(0, 0, 0, 0.15)",
          }),
          menuList: (styles) => ({
            ...styles,
            padding: "0",
          }),
        }}
      />
      {isTouched && (
        <div className="invalid-feedback">{defaultErrorMessage}</div>
      )}
    </div>
  );
}
