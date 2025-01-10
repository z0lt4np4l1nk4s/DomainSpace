import { useEffect, useState } from "react";
import {
  ContentFilterComponent,
  ContentHolderComponent,
  DefaultButton,
  FooterComponent,
  Loader,
  ModalComponent,
  NavBar,
  NoItemsComponent,
  PaginationComponent,
} from "../../components";
import {
  ContentFilter,
  ContentModel,
  PagedList,
  PageRouteEnum,
  SignalRMethodsEnum,
} from "../../types";
import { ContentService, SignalRService } from "../../services";
import { useNavigate } from "react-router-dom";
import { ToastUtil } from "../../utils";

const contentService = new ContentService();
const signalrService = new SignalRService();

export default function ContentPage() {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);
  const [contentFilter, setContentFilter] = useState<ContentFilter>({
    pageNumber: 1,
    pageSize: 10,
  });
  const [contentPaged, setContentPaged] = useState<PagedList<ContentModel>>();
  const [showRemoveModal, setShowRemoveModal] = useState(false);
  const [selectedContent, setSelectedContent] = useState<ContentModel>();

  useEffect(() => {
    setIsLoading(true);
    const getContentAsync = async () => {
      const response = await contentService.getPagedAsync(contentFilter);

      if (response.isSuccess) {
        setContentPaged(response.result!);
      }

      setIsLoading(false);
    };

    getContentAsync();
  }, [contentFilter]);

  const deleteContentAsync = async () => {
    const response = await contentService.deleteAsync(selectedContent?.id!);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Content removed successfully.");
      setShowRemoveModal(false);
      setContentFilter({
        ...contentFilter,
        pageNumber:
          contentPaged?.items?.length === 1 && contentFilter.pageNumber! > 1
            ? contentFilter.pageNumber! - 1
            : contentFilter.pageNumber,
      });
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  useEffect(() => {
    const connection = signalrService.buildConnection();

    connection?.on(SignalRMethodsEnum.LikeCountChanged, (data) => {
      setContentPaged((prevContentPaged) => {
        if (!prevContentPaged) return prevContentPaged;

        const updatedItems = prevContentPaged?.items?.map((item) => {
          return item.id === data.contentId
            ? new ContentModel({ ...item, likeCount: data.count })
            : item;
        });

        return {
          ...prevContentPaged,
          items: updatedItems,
        };
      });
    });

    return () => {
      connection?.off(SignalRMethodsEnum.LikeCountChanged);
    };
  }, []);

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <div className="container mt-3">
            <div className="d-flex justify-content-between align-items-center">
              <h2 className="mb-3">Latest posts</h2>
              <div>
                <DefaultButton
                  size="lg"
                  width={150}
                  onClick={() => {
                    navigate(PageRouteEnum.PublishContent);
                  }}
                >
                  Publish
                </DefaultButton>
              </div>
            </div>
            <div className="d-flex justify-content-end">
              <ContentFilterComponent
                filter={contentFilter}
                onChange={(filter) => {
                  console.log(filter);
                  setContentFilter(filter);
                }}
              />
            </div>
            <ContentHolderComponent
              items={contentPaged?.items || []}
              onDeleteClick={(item) => {
                setSelectedContent(item);
                setShowRemoveModal(true);
              }}
            />
            {(contentPaged?.items || []).length === 0 && <NoItemsComponent />}
            {(contentPaged?.lastPage || 1) > 1 && (
              <PaginationComponent
                onPageChange={(page) => {
                  setContentFilter({ ...contentFilter, pageNumber: page });
                }}
                currentPage={contentPaged?.pageNumber || 1}
                pageCount={contentPaged?.lastPage || 1}
              />
            )}
          </div>
        </Loader>
        <ModalComponent
          title={"Remove post"}
          show={showRemoveModal}
          onShowChange={setShowRemoveModal}
          onConfirm={deleteContentAsync}
        >
          <p>{`Are you sure you want to remove '${selectedContent?.title}'?`}</p>
        </ModalComponent>
      </div>
      <FooterComponent />
    </div>
  );
}
