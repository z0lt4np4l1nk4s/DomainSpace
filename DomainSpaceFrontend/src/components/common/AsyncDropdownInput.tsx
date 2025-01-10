import { useEffect, useState } from "react";
import AsyncSelect from "react-select/async";
import { DropdownItemModel } from "../../types";
import { ActionMeta } from "react-select";

export default function AsyncDropdownInput({
  name,
  title,
  loadOptions,
  onChange,
  value,
  width,
  defaultErrorMessage,
  onValidationChange,
  validate,
}: {
  name: string;
  title: string;
  loadOptions: (inputValue: string) => Promise<DropdownItemModel[]>;
  onChange: (
    option: DropdownItemModel | null,
    actionMeta: ActionMeta<DropdownItemModel>
  ) => void;
  defaultOptions?: DropdownItemModel[];
  value?: DropdownItemModel | null;
  width?: number;
  defaultErrorMessage?: string;
  onValidationChange?: (name: string, isValid: boolean) => void;
  validate?: boolean;
}) {
  const [isTouched, setIsTouched] = useState(false);
  const [isValid, setIsValid] = useState(true);
  const [inputValue, setInputValue] = useState<DropdownItemModel | null>(
    value || null
  );

  useEffect(() => {
    const isFormInvalid = validate === false ? false : !inputValue;
    setIsValid(!isFormInvalid);
    if (onValidationChange) {
      onValidationChange(name, !isFormInvalid);
    }
  }, [inputValue, name, onValidationChange]);

  return (
    <div className="form-group mb-3 mt-3" style={{ width: width || "100%" }}>
      {title && (
        <div className="mb-1">
          <label htmlFor={name} className="form-label">
            {title}
          </label>
        </div>
      )}
      <AsyncSelect
        name={name}
        cacheOptions
        loadOptions={loadOptions}
        defaultOptions
        isSearchable={true}
        onChange={(value, meta) => {
          setInputValue(value as DropdownItemModel);
          onChange(value, meta);
        }}
        value={value}
        className={`${isTouched && !isValid ? "is-invalid" : ""}`}
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
        onFocus={() => {
          if (!isTouched) setIsTouched(true);
        }}
        controlShouldRenderValue={true}
        pageSize={15}
      />
      {isTouched && !isValid && (
        <div className="invalid-feedback">{defaultErrorMessage}</div>
      )}
    </div>
  );
}
