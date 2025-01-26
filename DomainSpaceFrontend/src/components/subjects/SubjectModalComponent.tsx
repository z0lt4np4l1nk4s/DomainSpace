import React, { useCallback, useEffect, useRef, useState } from "react";
import DefaultButton from "../common/DefaultButton";
import TextInput from "../common/TextInput";
import { RoleEnum, SubjectModel } from "../../types";
import { TokenService } from "../../services";

export default function SubjectModalComponent({
  title,
  show,
  width,
  item,
  onShowChange,
  onCancel,
  onConfirm,
}: {
  title: string;
  show: boolean;
  width?: number;
  item?: SubjectModel;
  onShowChange: (show: boolean) => void;
  onCancel?: () => void;
  onConfirm?: (item: SubjectModel) => void;
}) {
  const modalRef = useRef<HTMLDivElement>(null);
  const [formData, setFormData] = useState<SubjectModel>(item || {});
  const [formValidations, setFormValidations] = useState({
    name: false,
    domain: false,
  });
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (item) {
      setFormData(item);
      setFormValidations({ name: !!item.name, domain: !!item.domain });
    } else {
      setFormData({});
      setFormValidations({ name: false, domain: false });
    }
  }, [item]);

  const handleClickOutside = (event: React.MouseEvent<HTMLDivElement>) => {
    if (modalRef.current && !modalRef.current.contains(event.target as Node)) {
      onShowChange(false);
    }
  };

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
  };

  const checkIfFormValid = () => {
    console.log(formValidations);
    const isFormValid = Object.entries(formValidations).every(
      ([key, isValid]) => {
        if (
          ["domain"].includes(key) &&
          !TokenService.isInRole(RoleEnum.Admin)
        ) {
          return true;
        }
        return isValid;
      }
    );
    return isFormValid;
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

  return (
    <div>
      {show && (
        <div
          className="modal d-flex align-items-center justify-content-center"
          tabIndex={-1}
          style={{
            display: "block",
            backgroundColor: "rgba(0, 0, 0, 0.5)",
          }}
          onClick={handleClickOutside}
        >
          <div
            className="modal-dialog"
            style={{
              width: "100%",
              maxWidth: width ? `${width}px` : "400px",
              padding: "0 16px",
              boxSizing: "border-box",
            }}
          >
            <div className="modal-content" ref={modalRef}>
              <div className="modal-header">
                <h5 className="modal-title">{title}</h5>
              </div>
              <div className="modal-body">
                <TextInput
                  title={"Name"}
                  name="name"
                  value={formData?.name}
                  onChange={handleInputChange}
                  onValidate={(value: string) => {
                    if (!value) {
                      return "Please enter the name.";
                    }

                    return undefined;
                  }}
                  onValidationChange={handleValidationChange}
                />
                {TokenService.isInRole(RoleEnum.Admin) && (
                  <TextInput
                    title={"Domain"}
                    name="domain"
                    value={formData?.domain}
                    onChange={handleInputChange}
                    onValidate={(value: string) => {
                      if (!value) {
                        return "Please enter the domain.";
                      }

                      return undefined;
                    }}
                    onValidationChange={handleValidationChange}
                  />
                )}
              </div>
              <div className="modal-footer">
                <DefaultButton
                  color="secondary"
                  onClick={() => {
                    if (onCancel) {
                      onCancel();
                    }
                    onShowChange(false);
                  }}
                >
                  {"Close"}
                </DefaultButton>
                <DefaultButton
                  disabled={!checkIfFormValid() || isLoading}
                  onClick={() => {
                    if (onConfirm && !isLoading) {
                      setIsLoading(true);
                      onConfirm(formData!);
                      setIsLoading(false);
                    }
                  }}
                  color="success"
                >
                  {"Confirm"}
                </DefaultButton>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
