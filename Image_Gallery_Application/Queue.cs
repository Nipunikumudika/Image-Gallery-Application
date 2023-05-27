using System.Diagnostics;


namespace Image_Gallery_Application
{
	public class Queue
    {
        public int size;       //marks the size of the queue
        public int capacity;   //marks the size of the queue
        public int front;      //marks of the start of the queue
        public int rear;       //marks of the end of the queue
        public string[] data;      //array

        public Queue(int cap)                                
        {
            size = 0;
            capacity = cap;
            data = new string[capacity];
            front = 0;
            rear = 0;
        }

        public void enQueue(string val)
        { //inserting a value
            if (size >= capacity)
            {     //if(front==rear)
                Debug.WriteLine("Queue is full");
            }
            else
            {
                data[rear] = val;
                rear = (rear + 1) % capacity;
                size++;
            }
        }

        public string deQueue()
        {
            if (size == 0)
            {
                Debug.WriteLine("Queue is empty");
                return null;
            }
            else
            {
                Debug.WriteLine(front);
                string temp = data[front];
                front = (front + 1) % capacity;
                size--;
                return temp;
            }
        }

        public void print()
        {
            int index = front;
            for (int i = 0; i < size; i++)
            {
                Debug.WriteLine(data[index]);
                index = (index + 1) % capacity;
            }
        }

    }

}
