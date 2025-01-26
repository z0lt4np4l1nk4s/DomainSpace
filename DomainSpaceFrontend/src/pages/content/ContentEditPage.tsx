import { ChangeEvent, useCallback, useEffect, useState } from "react";
import {
  AsyncDropdownInput,
  DefaultButton,
  FooterComponent,
  InputForm,
  Loader,
  NavBar,
  TextInput,
} from "../../components";
import { ContentService, SubjectService } from "../../services";
import { useNavigate, useParams } from "react-router-dom";
import {
  DropdownItemModel,
  PageRouteEnum,
  SubjectFilter,
  UpdateContentModel,
} from "../../types";
import { ToastUtil } from "../../utils";
import { SingleValue } from "react-select";
import { FaTimes } from "react-icons/fa";

const contentService = new ContentService();
const subjectService = new SubjectService();

export default function ContentEditPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitLoading, setIsSubmitLoading] = useState(false);
  const params = useParams();

  const navigate = useNavigate();
  const [formData, setFormData] = useState<UpdateContentModel>({});
  const [subjectFilter, setSubjectFilter] = useState<SubjectFilter>({
    pageSize: 100,
  });
  const [selectedSubject, setSelectedSubject] =
    useState<DropdownItemModel | null>(null);

  const [formValidations, setFormValidations] = useState({
    title: true,
    subjectId: true,
    domain: true,
  });

  const checkIfFormIsValid = () => {
    const isFormValid = Object.entries(formValidations).every(
      ([key, isValid]) => {
        return isValid;
      }
    );

    return (
      isFormValid &&
      (!!formData.text ||
        (formData.localFiles && formData.localFiles.length > 0) ||
        (formData.oldFiles && formData.oldFiles.length > 0))
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

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
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
        localFiles: Array.from(files),
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

    setIsSubmitLoading(true);

    const response = await contentService.updateAsync(formData);

    if (response.isSuccess) {
      navigate(PageRouteEnum.Home);
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }

    setIsSubmitLoading(false);
  };

  useEffect(() => {
    const getContentAsync = async () => {
      const response = await contentService.getByIdAsync(params.id ?? "");

      if (response.isSuccess) {
        const content = response.result!;
        setFormData(
          new UpdateContentModel({
            id: content.id,
            title: content.title,
            text: content.text,
            subjectId: content.subjectId,
            oldFiles: content.files,
          })
        );
        return response.result!;
      }

      return null;
    };

    const fetchData = async () => {
      const content = await getContentAsync();

      if (content) {
        const subjectResponse = await subjectService.getByIdAsync(
          content.subjectId!
        );

        if (subjectResponse.isSuccess) {
          setSelectedSubject(
            new DropdownItemModel(
              content.subjectId!,
              subjectResponse.result!.name || ""
            )
          );
        }
      }

      setIsLoading(false);
    };

    fetchData();
  }, [params.id]);

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <div className="container mt-3">
            <div className="d-flex flex-grow-1 justify-content-center align-items-top mt-5">
              <div className="container mb-5">
                <h2 className="text-center mb-4">Edit Content</h2>
                <div className="row justify-content-center">
                  <div className="col-md-6">
                    <InputForm onSubmit={handleSubmit}>
                      <AsyncDropdownInput
                        name="subjectId"
                        title="Subject"
                        value={selectedSubject}
                        loadOptions={getSubjectsAsync}
                        onChange={(result) => {
                          setFormData({
                            ...formData,
                            subjectId: result
                              ? (result as SingleValue<DropdownItemModel>)!
                                  .value!
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
                        <label htmlFor="text">Text</label>
                        <textarea
                          id="text"
                          name="text"
                          className="form-control"
                          rows={5}
                          onChange={(
                            event: ChangeEvent<HTMLTextAreaElement>
                          ) => {
                            setFormData({
                              ...formData,
                              text: event.target.value,
                            });
                          }}
                          value={formData?.text || ""}
                        />
                      </div>
                      <div className="form-group mt-3">
                        <label htmlFor="files">Upload Files (Optional)</label>
                        <input
                          id="files"
                          name="files"
                          type="file"
                          className="form-control"
                          multiple
                          onChange={handleFileChange}
                        />
                      </div>
                      <div className="form-group mt-3">
                        <label>Existing Files</label>
                        {formData.oldFiles && formData.oldFiles.length > 0 ? (
                          <div className="row">
                            {formData.oldFiles.map((file, index) => (
                              <div key={index} className="col-6 mb-2">
                                <div className="d-flex justify-content-between align-items-center p-2 rounded bg-secondary">
                                  <span
                                    className="text-truncate"
                                    title={file.name}
                                  >
                                    {file.name}
                                  </span>
                                  <button
                                    type="button"
                                    className="btn btn-danger btn-sm ms-2"
                                    onClick={() => {
                                      setFormData((prevFormData) => ({
                                        ...prevFormData,
                                        oldFiles: prevFormData.oldFiles?.filter(
                                          (_, i) => i !== index
                                        ),
                                      }));
                                    }}
                                  >
                                    <FaTimes />
                                  </button>
                                </div>
                              </div>
                            ))}
                          </div>
                        ) : (
                          <div className="d-flex justify-content-center align-items-center mt-3">
                            <p className="text-muted">No files available.</p>
                          </div>
                        )}
                      </div>
                      <div className="mt-3">
                        <DefaultButton
                          type="submit"
                          disabled={!checkIfFormIsValid()}
                          isLoading={isSubmitLoading}
                        >
                          Save changes
                        </DefaultButton>
                      </div>
                    </InputForm>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </Loader>
      </div>
      <FooterComponent />
    </div>
  );
}
