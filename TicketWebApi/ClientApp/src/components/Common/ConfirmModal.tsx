import * as React from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from "reactstrap";

type propsType = {
  isOpen?: boolean;
  title?: string;
  description?: string;
  handleConfirm: any;
  handleCancel: any;
};

export const ConfirmModal = (props: propsType) => {
  const { isOpen, title, description, handleConfirm, handleCancel } = props;
  return (
    <Modal isOpen={isOpen}>
      <ModalHeader>{title}</ModalHeader>
      <ModalBody>{description}</ModalBody>
      <ModalFooter>
        <Button color="primary" onClick={handleConfirm}>
          Confirm
        </Button>{" "}
        <Button color="secondary" onClick={handleCancel}>
          Cancel
        </Button>
      </ModalFooter>
    </Modal>
  );
};

export default ConfirmModal;
