import { useEffect, useState } from "react";
import {
  DefaultButton,
  FooterComponent,
  Loader,
  ModalComponent,
  NavBar,
  NoItemsComponent,
  PaginationComponent,
  SubjectModalComponent,
  SubjectsFilterComponent,
  SubjectsTableComponent,
} from "../../components";
import { PagedList, SubjectFilter, SubjectModel } from "../../types";
import { SubjectService } from "../../services";
import { ToastUtil } from "../../utils";

const subjectService = new SubjectService();

export default function SubjectsPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [showRemoveModal, setShowRemoveModal] = useState(false);
  const [selectedSubject, setSelectedSubject] = useState<SubjectModel>();
  const [subjectFilter, setSubjectFilter] = useState<SubjectFilter>({
    pageNumber: 1,
    pageSize: 10,
  });
  const [subjectsPaged, setSubjectsPaged] = useState<PagedList<SubjectModel>>();

  useEffect(() => {
    setIsLoading(true);

    const getSubjectsAsync = async () => {
      const response = await subjectService.getPagedAsync(subjectFilter);

      if (response.isSuccess) {
        setSubjectsPaged(response.result!);
      }

      setIsLoading(false);
    };

    getSubjectsAsync();
  }, [subjectFilter]);

  const addSubjectAsync = async (item: SubjectModel) => {
    const response = await subjectService.addAsync(item);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Subject added successfully.");
      setShowAddModal(false);
      setSubjectFilter({ ...subjectFilter, pageNumber: 1 });
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  const updateSubjectAsync = async (item: SubjectModel) => {
    const response = await subjectService.updateAsync(item);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Subject updated successfully.");
      setShowUpdateModal(false);
      setSubjectFilter({ ...subjectFilter });
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  const deleteSubjectAsync = async () => {
    const response = await subjectService.deleteAsync(selectedSubject?.id!);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Subject removed successfully.");
      setShowAddModal(false);
      setSubjectFilter({
        ...subjectFilter,
        pageNumber:
          subjectsPaged?.items?.length === 1 && subjectFilter.pageNumber! > 1
            ? subjectFilter.pageNumber! - 1
            : subjectFilter.pageNumber,
      });
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <div className="container mt-3">
            <div className="d-flex justify-content-between align-items-center">
              <h2 className="mb-3">Subjects</h2>
              <div>
                <DefaultButton
                  size="lg"
                  onClick={() => {
                    setShowAddModal(true);
                  }}
                >
                  New subject
                </DefaultButton>
              </div>
            </div>
            <div className="d-flex justify-content-end">
              <SubjectsFilterComponent
                filter={subjectFilter}
                onChange={(filter) => {
                  setSubjectFilter(filter);
                }}
              />
            </div>
            <SubjectsTableComponent
              onEditClick={(item: SubjectModel) => {
                setSelectedSubject(item);
                setShowUpdateModal(true);
              }}
              onRemoveClick={(item: SubjectModel) => {
                setSelectedSubject(item);
                setShowRemoveModal(true);
              }}
              items={subjectsPaged?.items || []}
            />
            {(subjectsPaged?.items || []).length === 0 && <NoItemsComponent />}
            {(subjectsPaged?.lastPage || 1) > 1 && (
              <PaginationComponent
                onPageChange={(page) => {
                  setSubjectFilter({ ...subjectFilter, pageNumber: page });
                }}
                currentPage={subjectsPaged?.pageNumber || 1}
                pageCount={subjectsPaged?.lastPage || 1}
              />
            )}
          </div>
        </Loader>
        <ModalComponent
          title={"Remove subject"}
          show={showRemoveModal}
          onShowChange={setShowRemoveModal}
          onConfirm={deleteSubjectAsync}
        >
          <p>{`Are you sure you want to remove the subject '${selectedSubject?.name}'?`}</p>
        </ModalComponent>
        <SubjectModalComponent
          title={"Add subject"}
          show={showAddModal}
          onShowChange={setShowAddModal}
          onConfirm={addSubjectAsync}
        />
        <SubjectModalComponent
          title={"Update subject"}
          show={showUpdateModal}
          item={selectedSubject}
          onShowChange={setShowUpdateModal}
          onConfirm={updateSubjectAsync}
        />
      </div>
      <FooterComponent />
    </div>
  );
}
