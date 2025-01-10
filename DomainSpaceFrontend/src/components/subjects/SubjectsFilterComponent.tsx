import { useState } from "react";
import { SubjectFilter } from "../../types";
import { DefaultButton, InputForm, TextInput } from "../common";

export default function SubjectsFilterComponent({
  filter,
  onChange,
}: {
  filter: SubjectFilter;
  onChange?: (filter: SubjectFilter) => void;
}) {
  const [formData, setFormData] = useState<SubjectFilter>(filter);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(
      new SubjectFilter({
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
        <div
          className="d-flex btn-group gap-3 align-items-end"
          style={{ marginBottom: "17px" }}
        >
          <DefaultButton type="submit" width={80}>
            {"Filter"}
          </DefaultButton>
          {filter.query && (
            <DefaultButton
              type="button"
              width={120}
              color="secondary"
              onClick={() => {
                if (onChange) {
                  onChange({
                    ...formData,
                    query: undefined,
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
