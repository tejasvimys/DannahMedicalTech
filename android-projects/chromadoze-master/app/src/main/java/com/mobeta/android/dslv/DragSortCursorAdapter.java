package com.mobeta.android.dslv;

import java.util.ArrayList;

import android.content.Context;
import android.database.Cursor;
import android.util.Log;
import android.util.SparseIntArray;
import android.view.View;
import android.view.ViewGroup;
import android.support.v4.widget.CursorAdapter;

/**
 * A subclass of {@link android.widget.CursorAdapter} that provides reordering
 * of the elements in the Cursor based on completed drag-sort operations. The
 * reordering is a simple mapping of list positions into Cursor positions (the
 * Cursor is unchanged). To persist changes made by drag-sorts, one can retrieve
 * the mapping with the {@link #getCursorPositions()} method, which returns the
 * reordered list of Cursor positions.
 * 
 * An instance of this class is passed to
 * {@link DragSortListView#setAdapter(ListAdapter)} and, since this class
 * implements the {@link DragSortListView.DragSortListener} interface, it is
 * automatically set as the DragSortListener for the DragSortListView instance.
 */
public abstract class DragSortCursorAdapter extends CursorAdapter implements
        DragSortListView.DragSortListener {

    public static final int REMOVED = -1;

    /**
     * Key is ListView position, value is Cursor position
     */
    private SparseIntArray mListMapping = new SparseIntArray();

    private ArrayList<Integer> mRemovedCursorPositions = new ArrayList<Integer>();

    public DragSortCursorAdapter(Context context, Cursor c) {
        super(context, c);
        Log.i("main", "Inside constructor of DragSortCursorAdapter");
    }

    public DragSortCursorAdapter(Context context, Cursor c, boolean autoRequery) {
        super(context, c, autoRequery);
        Log.i("main", "Inside constructor 2 of DragSortCursorAdapter");
    }

    public DragSortCursorAdapter(Context context, Cursor c, int flags) {
        super(context, c, flags);
        Log.i("main", "Inside constructor 3 of DragSortCursorAdapter");
    }

    /**
     * Swaps Cursor and clears list-Cursor mapping.
     * 
     * @see android.widget.CursorAdapter#swapCursor(android.database.Cursor)
     */
    @Override
    public Cursor swapCursor(Cursor newCursor) {

        Log.i("main", "Inside swapCursor of DragSortCursorAdapter");
        Cursor old = super.swapCursor(newCursor);
        resetMappings();
        return old;
    }

    /**
     * Changes Cursor and clears list-Cursor mapping.
     * 
     * @see android.widget.CursorAdapter#changeCursor(android.database.Cursor)
     */
    @Override
    public void changeCursor(Cursor cursor) {
        Log.i("main", "Inside changeCursor of DragSortCursorAdapter");
        super.changeCursor(cursor);
        resetMappings();
    }

    /**
     * Resets list-cursor mapping.
     */
    public void reset() {
        Log.i("main", "Inside reset of DragSortCursorAdapter");
        resetMappings();
        notifyDataSetChanged();
    }

    private void resetMappings() {
        Log.i("main", "Inside resetMappings of DragSortCursorAdapter");
        mListMapping.clear();
        mRemovedCursorPositions.clear();
    }

    @Override
    public Object getItem(int position) {
        Log.i("main", "Inside getItem of DragSortCursorAdapter");
        return super.getItem(mListMapping.get(position, position));
    }

    @Override
    public long getItemId(int position) {
        Log.i("main", "Inside getItemId of DragSortCursorAdapter");
        return super.getItemId(mListMapping.get(position, position));
    }

    @Override
    public View getDropDownView(int position, View convertView, ViewGroup parent) {
        Log.i("main", "Inside getDropDownView of DragSortCursorAdapter");
        return super.getDropDownView(mListMapping.get(position, position),
                convertView, parent);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        Log.i("main", "Inside getView of DragSortCursorAdapter");
        return super.getView(mListMapping.get(position, position), convertView,
                parent);
    }

    /**
     * On drop, this updates the mapping between Cursor positions and ListView
     * positions. The Cursor is unchanged. Retrieve the current mapping with
     * {@link getCursorPositions()}.
     * 
     * @see DragSortListView.DropListener#drop(int, int)
     */
    @Override
    public void drop(int from, int to) {
        Log.i("main", "Inside drop of DragSortCursorAdapter");
        if (from != to) {
            int cursorFrom = mListMapping.get(from, from);

            if (from > to) {
                for (int i = from; i > to; --i) {
                    mListMapping.put(i, mListMapping.get(i - 1, i - 1));
                }
            } else {
                for (int i = from; i < to; ++i) {
                    mListMapping.put(i, mListMapping.get(i + 1, i + 1));
                }
            }
            mListMapping.put(to, cursorFrom);

            cleanMapping();
            notifyDataSetChanged();
        }
    }

    /**
     * On remove, this updates the mapping between Cursor positions and ListView
     * positions. The Cursor is unchanged. Retrieve the current mapping with
     * {@link getCursorPositions()}.
     * 
     * @see DragSortListView.RemoveListener#remove(int)
     */
    @Override
    public void remove(int which) {
        Log.i("main", "Inside remove of DragSortCursorAdapter");
        int cursorPos = mListMapping.get(which, which);
        if (!mRemovedCursorPositions.contains(cursorPos)) {
            mRemovedCursorPositions.add(cursorPos);
        }

        int newCount = getCount();
        for (int i = which; i < newCount; ++i) {
            mListMapping.put(i, mListMapping.get(i + 1, i + 1));
        }

        mListMapping.delete(newCount);

        cleanMapping();
        notifyDataSetChanged();
    }

    /**
     * Does nothing. Just completes DragSortListener interface.
     */
    @Override
    public void drag(int from, int to) {
        Log.i("main", "Inside drag of DragSortCursorAdapter");
        // do nothing
    }

    /**
     * Remove unnecessary mappings from sparse array.
     */
    private void cleanMapping() {
        Log.i("main", "Inside cleanMapping of DragSortCursorAdapter");
        ArrayList<Integer> toRemove = new ArrayList<Integer>();

        int size = mListMapping.size();
        for (int i = 0; i < size; ++i) {
            if (mListMapping.keyAt(i) == mListMapping.valueAt(i)) {
                toRemove.add(mListMapping.keyAt(i));
            }
        }

        size = toRemove.size();
        for (int i = 0; i < size; ++i) {
            mListMapping.delete(toRemove.get(i));
        }
    }

    @Override
    public int getCount() {
        Log.i("main", "Inside getCount of DragSortCursorAdapter");
        return super.getCount() - mRemovedCursorPositions.size();
    }

    /**
     * Get the Cursor position mapped to by the provided list position (given
     * all previously handled drag-sort operations).
     * 
     * @param position
     *            List position
     * 
     * @return The mapped-to Cursor position
     */
    public int getCursorPosition(int position)
    {
        Log.i("main", "Inside getCursorPosition of DragSortCursorAdapter");
        return mListMapping.get(position, position);
    }

    /**
     * Get the current order of Cursor positions presented by the list.
     */
    public ArrayList<Integer> getCursorPositions() {
        Log.i("main", "Inside getCursorPosition of DragSortCursorAdapter");
        ArrayList<Integer> result = new ArrayList<Integer>();

        for (int i = 0; i < getCount(); ++i) {
            result.add(mListMapping.get(i, i));
        }

        return result;
    }

    /**
     * Get the list position mapped to by the provided Cursor position. If the
     * provided Cursor position has been removed by a drag-sort, this returns
     * {@link #REMOVED}.
     * 
     * @param cursorPosition
     *            A Cursor position
     * @return The mapped-to list position or REMOVED
     */
    public int getListPosition(int cursorPosition) {
        Log.i("main", "Inside getListPosition of DragSortCursorAdapter");
        if (mRemovedCursorPositions.contains(cursorPosition)) {
            return REMOVED;
        }

        int index = mListMapping.indexOfValue(cursorPosition);
        if (index < 0) {
            return cursorPosition;
        } else {
            return mListMapping.keyAt(index);
        }
    }

}
