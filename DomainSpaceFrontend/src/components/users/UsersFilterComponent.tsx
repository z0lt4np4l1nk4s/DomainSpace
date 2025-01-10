import React, { useState } from "react";
import { DropdownItemModel, RoleEnum, UserFilter } from "../../types";
import { DefaultButton, DropdownInput, InputForm, TextInput } from "../common";
import { SingleValue } from "react-select";

export default function UsersFilterComponent({
  filter,
  onChange,
}: {
  filter: UserFilter;
  onChange?: (filter: UserFilter) => void;
}) {
  const [formData, setFormData] = useState<UserFilter>(filter);
  const employeeStatusOptions = Object.values(RoleEnum)
    .filter((value) => typeof value === "string")
    .map((value) => new DropdownItemModel(value, value));

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(
      new UserFilter({
        ...formData,
        [event.target.name]: event.target.value,
      })
    );
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (onChange) {
      onChange(formData);
    }
  };

  return (
    <InputForm onSubmit={handleSubmit}>
      <div className="d-flex flex-wrap gap-3">
        <TextInput
          name="query"
          title="Search"
          value={formData.query}
          width={170}
          onChange={handleInputChange}
        />
        <DropdownInput
          name="role"
          title="Role"
          width={170}
          value={
            formData.roles
              ? new DropdownItemModel(formData.roles, formData.roles)
              : undefined
          }
          options={employeeStatusOptions}
          onChange={(result) => {
            const value = (result as SingleValue<DropdownItemModel>)?.value;
            setFormData(
              new UserFilter({
                ...formData,
                roles: value,
              })
            );
          }}
        />
        <div
          className="d-flex btn-group gap-3 align-items-end"
          style={{ marginBottom: "17px" }}
        >
          <DefaultButton type="submit" width={80}>
            {"Filter"}
          </DefaultButton>
          {(filter.query || filter.roles) && (
            <DefaultButton
              type="button"
              width={120}
              color="secondary"
              onClick={() => {
                if (onChange) {
                  onChange({
                    ...formData,
                    query: undefined,
                    roles: undefined,
                  });
                }
              }}
            >
              {"Clear filter"}
            </DefaultButton>
          )}
        </div>
      </div>
    </InputForm>
  );
}
