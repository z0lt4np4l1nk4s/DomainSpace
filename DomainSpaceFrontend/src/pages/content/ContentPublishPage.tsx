import { ChangeEvent, useCallback, useState } from "react";
import {
  AsyncDropdownInput,
  DefaultButton,
  FooterComponent,
  InputForm,
  NavBar,
  TextInput,
} from "../../components";
import {
  AddContentModel,
  DropdownItemModel,
  PageRouteEnum,
  RoleEnum,
  SubjectFilter,
} from "../../types";
import {
  ContentService,
  SubjectService,
  UserDataService,
} from "../../services";
import { SingleValue } from "react-select";
import { ToastUtil } from "../../utils";
import { useNavigate } from "react-router-dom";

const contentService = new ContentService();
const subjectService = new SubjectService();

export default function ContentPublishPage() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<AddContentModel>({});
  const [subjectFilter, setSubjectFilter] = useState<SubjectFilter>({});

  const [formValidations, setFormValidations] = useState({
    title: false,
    subjectId: false,
    domain: false,
  });

  const checkIfFormIsValid = () => {
    const isFormValid = Object.entries(formValidations).every(
      ([key, isValid]) => {
        if (
          ["domain"].includes(key) &&
          !UserDataService.isInRole(RoleEnum.Admin)
        ) {
          return true;
        }
        return isValid;
      }
    );

    return (
      isFormValid &&
      (!!formData.text || (formData.files && formData.files.length > 0))
    );
  };

  const handleValidationChange = useCallback(
    (name: string, isValid: boolean) => {
      setFormValidations((prevValidations) => ({
        ...prevValidations,
        [name]: isValid,
      }));
    },
    []
  );

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
  };

  const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;

    if (files) {
      setFormData({
        ...formData,
        files: Array.from(files),
      });
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

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!checkIfFormIsValid()) {
      return;
    }

    const response = await contentService.addAsync(formData);

    if (response.isSuccess) {
      navigate(PageRouteEnum.Home);
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <div className="container mt-3">
          <div className="d-flex flex-grow-1 justify-content-center align-items-top mt-5">
            <div className="container mb-5">
              <h2 className="text-center mb-4">Publish new Content</h2>
              <div className="row justify-content-center">
                <div className="col-md-6">
                  <InputForm onSubmit={handleSubmit}>
                    {UserDataService.isInRole(RoleEnum.Admin) && (
                      <TextInput
                        title="Domain"
                        name="domain"
                        onChange={handleInputChange}
                        value={formData.domain}
                        onValidate={(value: string) => {
                          if (!value) {
                            return "Please enter the domain.";
                          }

                          return undefined;
                        }}
                        onValidationChange={handleValidationChange}
                      />
                    )}
                    <AsyncDropdownInput
                      name="subjectId"
                      title="Subject"
                      loadOptions={getSubjectsAsync}
                      onChange={(result) => {
                        setFormData({
                          ...formData,
                          subjectId: result
                            ? (result as SingleValue<DropdownItemModel>)!.value!
                            : undefined,
                        });
                      }}
                      defaultErrorMessage="Please select the subject."
                      onValidationChange={handleValidationChange}
                    />
                    <TextInput
                      title="Title"
                      name="title"
                      onChange={handleInputChange}
                      value={formData.title}
                      onValidate={(value: string) => {
                        if (!value) {
                          return "Please enter the title.";
                        }

                        return undefined;
                      }}
                      onValidationChange={handleValidationChange}
                    />
                    <div className="form-group mt-3">
                      <div className="mb-1">
                        <label htmlFor={"text"} className="form-label">
                          {"Text"}
                        </label>
                      </div>
                      <textarea
                        id="text"
                        name="text"
                        className="form-control"
                        rows={5}
                        onChange={(event: ChangeEvent<HTMLTextAreaElement>) => {
                          setFormData({
                            ...formData,
                            text: event.target.value,
                          });
                        }}
                        value={formData?.text || ""}
                      />
                    </div>
                    <div className="form-group mt-3">
                      <label htmlFor="files">Upload Files</label>
                      <input
                        id="files"
                        name="files"
                        type="file"
                        className="form-control"
                        multiple
                        onChange={handleFileChange}
                      />
                    </div>
                    <div className="mt-4">
                      <DefaultButton
                        type="submit"
                        size="lg"
                        disabled={!checkIfFormIsValid()}
                      >
                        Publish
                      </DefaultButton>
                    </div>
                  </InputForm>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <FooterComponent />
    </div>
  );
}
