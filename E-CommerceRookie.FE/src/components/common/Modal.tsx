import type { ReactNode } from 'react'

type ModalProps = {
  isOpen: boolean
  title: string
  eyebrow?: string
  onClose: () => void
  children: ReactNode
}

export default function Modal({
  isOpen,
  title,
  eyebrow,
  onClose,
  children,
}: ModalProps) {
  if (!isOpen) return null

  return (
    <div className="modal-overlay" role="presentation">
      <div className="modal-card" role="dialog" aria-modal="true">
        <div className="modal-header">
          <div>
            {eyebrow ? <p className="eyebrow">{eyebrow}</p> : null}
            <h2>{title}</h2>
          </div>
          <button className="ghost-button" onClick={onClose}>
            Close
          </button>
        </div>
        {children}
      </div>
    </div>
  )
}
