import React, { useCallback, useEffect, useState } from "react";
import { ContentFilter, DropdownItemModel, SubjectFilter } from "../../types";
import {
  AsyncDropdownInput,
  DefaultButton,
  InputForm,
  TextInput,
} from "../common";
import { SingleValue } from "react-select";
import { SubjectService } from "../../services";

const subjectService = new SubjectService();

export default function ContentFilterComponent({
  filter,
  onChange,
}: {
  filter: ContentFilter;
  onChange?: (filter: ContentFilter) => void;
}) {
  const [formData, setFormData] = useState<ContentFilter>(filter);
  const [subjectFilter, setSubjectFilter] = useState<SubjectFilter>({});
  const [selectedSubject, setSelectedSubject] = useState<DropdownItemModel>();

  useEffect(() => {
    const fetchSelectedSubject = async () => {
      if (filter.subjectIds) {
        const response = await subjectService.getByIdAsync(filter.subjectIds);
        if (response.isSuccess && response.result) {
          setSelectedSubject(
            new DropdownItemModel(
              response.result.id!,
              response.result.name || ""
            )
          );
        }
      } else {
        setSelectedSubject(undefined);
      }
    };

    fetchSelectedSubject();
  }, [formData.subjectIds]);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(
      new ContentFilter({
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

  const getSubjectsAsync = useCallback(
    async (inputValue: string): Promise<DropdownItemModel[]> => {
      const newFilter = new SubjectFilter(subjectFilter);
      newFilter.query = inputValue;

      if (subjectFilter.query !== inputValue) {
        newFilter.pageNumber = 1;
      } else {
        newFilter.pageNumber = (newFilter.pageNumber || 0) + 1;
      }

      setSubjectFilter(newFilter);
      const response = await subjectService.getPagedAsync(newFilter);

      if (response.isSuccess && response.result && response.result.items) {
        const options = response.result.items.map(
          (item) => new DropdownItemModel(item.id!, item.name || "")
        );
        return options;
      }
      return [];
    },
    [subjectFilter]
  );

  return (
    <InputForm onSubmit={handleSubmit}>
      <div className="d-flex flex-wrap gap-3">
        <TextInput
          name="query"
          title={"Search"}
          value={formData.query}
          width={170}
          onChange={handleInputChange}
        />

        <AsyncDropdownInput
          name="subject"
          title={"Subject"}
          width={170}
          loadOptions={getSubjectsAsync}
          value={selectedSubject}
          validate={false}
          onChange={(result) => {
            const value = (result as SingleValue<DropdownItemModel>)?.value;
            setFormData(
              new ContentFilter({
                ...formData,
                subjectIds: value,
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
          {(filter.query || filter.subjectIds) && (
            <DefaultButton
              type="button"
              width={120}
              color="secondary"
              onClick={() => {
                if (onChange) {
                  onChange({
                    ...formData,
                    query: undefined,
                    subjectIds: undefined,
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
