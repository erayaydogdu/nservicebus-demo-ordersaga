## NServiceBus Order Saga Example Send Email

The application processes orders, notifying customers by email, and the completion of the saga when the email notification is sent.


## Sequence Diagram

The following sequence diagram illustrates the flow of the order processing and send email system:

```mermaid
sequenceDiagram
    participant Client
    participant OrderManager
    participant NotificationManager
    participant TemplateEngine
    
    Client->>OrderManager: PlaceOrder
    OrderManager->>OrderManager: Start OrderSaga
    OrderManager->>NotificationManager: NotifyCustomer
    NotificationManager->>TemplateEngine: GenerateEmailBody
    TemplateEngine-->>NotificationManager: EmailBody
    NotificationManager-->>OrderManager: IEmailSent
    OrderManager->>OrderManager: MarkAsComplete

